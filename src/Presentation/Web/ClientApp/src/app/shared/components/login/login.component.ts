import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable, defer, from, take } from 'rxjs';

import { AuthenticationResultStatus, AuthorizeService } from '../../services';
import { ApplicationPaths, ReturnUrlType, QueryParameterNames, LoginActions, ApplicationName } from '../../constants';

// The main responsibility of this component is to handle the user's login process.
// This is the starting point for the login process. Any component that needs to authenticate
// a user can simply perform a redirect to this component with a returnUrl query parameter and
// let the component perform the login and return back to the return url.
@Component({
  selector: 'appc-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  public message = new BehaviorSubject<string>(null);

  constructor(private authorizeService: AuthorizeService, private activatedRoute: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    const action = this.activatedRoute.snapshot.url[1];
    switch (action.path) {
      case LoginActions.Login:
        this.login(this.getReturnUrl());
        break;
      case LoginActions.LoginCallback:
        this.processLoginCallback();
        break;
      case LoginActions.LoginFailed:
        const message = this.activatedRoute.snapshot.queryParamMap.get(QueryParameterNames.Message);
        this.message.next(message);
        break;
      case LoginActions.Profile:
        this.redirectToProfile();
        break;
      case LoginActions.Register:
        this.redirectToRegister();
        break;
      default:
        throw new Error(`Invalid action '${action}'`);
    }
  }

  private login(returnUrl: string) {
    const state: INavigationState = { returnUrl };
    let signIn$ = this.authorizeService.signIn(state);

    signIn$.pipe(take(1)).subscribe((result) => {
      this.message.next(undefined);
      switch (result.status) {
        case AuthenticationResultStatus.Redirect:
          break;
        case AuthenticationResultStatus.Success:
          this.navigateToReturnUrl(returnUrl).pipe(take(1)).subscribe();
          break;
        case AuthenticationResultStatus.Fail:
          defer(() =>
            this.router.navigate(ApplicationPaths.LoginFailedPathComponents, {
              queryParams: { [QueryParameterNames.Message]: result.message },
            }),
          )
            .pipe(take(1))
            .subscribe();
          break;
        default:
          throw new Error(`Invalid status result ${(result as any).status}.`);
      }
    });
  }

  private processLoginCallback() {
    const url = window.location.href;
    this.authorizeService
      .completeSignIn(url)
      .pipe(take(1))
      .subscribe((result) => {
        switch (result.status) {
          case AuthenticationResultStatus.Redirect:
            // There should not be any redirects as completeSignIn never redirects.
            throw new Error('Should not redirect.');
          case AuthenticationResultStatus.Success:
            this.navigateToReturnUrl(this.getReturnUrl(result.state)).pipe(take(1)).subscribe();
            break;
          case AuthenticationResultStatus.Fail:
            this.message.next(result.message);
            break;
        }
      });
  }

  private redirectToRegister(): any {
    this.redirectToApiAuthorizationPath(`${ApplicationPaths.IdentityRegisterPath}?${ReturnUrlType}=?client_id=${ApplicationName}`);
  }

  private redirectToProfile(): void {
    this.redirectToApiAuthorizationPath(`${ApplicationPaths.IdentityManagePath}?${ReturnUrlType}=?client_id=${ApplicationName}`);
  }

  private navigateToReturnUrl(returnUrl: string): Observable<boolean> {
    // It's important that we do a replace here so that we remove the callback uri with the
    // fragment containing the tokens from the browser history.
    return defer(() =>
      from(
        this.router.navigateByUrl(returnUrl, {
          replaceUrl: true,
        }),
      ),
    );
  }

  private getReturnUrl(state?: INavigationState): string {
    const fromQuery = (this.activatedRoute.snapshot.queryParams as INavigationState).returnUrl;
    // If the url is comming from the query string, check that is either
    // a relative url or an absolute url
    if (fromQuery && !(fromQuery.startsWith(`${window.location.origin}/`) || /\/[^\/].*/.test(fromQuery))) {
      // This is an extra check to prevent open redirects.
      throw new Error('Invalid return url. The return url needs to have the same origin as the current page.');
    }
    return (state && state.returnUrl) || fromQuery || ApplicationPaths.DefaultLoginRedirectPath;
  }

  private redirectToApiAuthorizationPath(apiAuthorizationPath: string) {
    // It's important that we do a replace here so that when the user hits the back arrow on the
    // browser they get sent back to where it was on the app instead of to an endpoint on this
    // component.
    window.location.replace(apiAuthorizationPath);
  }
}

interface INavigationState {
  [ReturnUrlType]: string;
}
