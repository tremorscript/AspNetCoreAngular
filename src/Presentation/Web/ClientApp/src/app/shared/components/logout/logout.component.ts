import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable, defer, from } from 'rxjs';
import { filter, mergeMap, take } from 'rxjs/operators';

import { AuthenticationResultStatus, AuthorizeService } from '../../services';
import { LogoutActions, ApplicationPaths, ReturnUrlType } from '../../constants';

// The main responsibility of this component is to handle the user's logout process.
// This is the starting point for the logout process, which is usually initiated when a
// user clicks on the logout button on the LoginMenu component.
@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css'],
})
export class LogoutComponent implements OnInit {
  public message = new BehaviorSubject<string>(null);

  constructor(private authorizeService: AuthorizeService, private activatedRoute: ActivatedRoute, private router: Router) {}

  async ngOnInit() {
    const action = this.activatedRoute.snapshot.url[1];
    switch (action.path) {
      case LogoutActions.Logout:
        if (!!window.history.state.local) {
          await this.logout(this.getReturnUrl());
        } else {
          // This prevents regular links to <app>/authentication/logout from triggering a logout
          this.message.next('The logout was not initiated from within the page.');
        }

        break;
      case LogoutActions.LogoutCallback:
        await this.processLogoutCallback();
        break;
      case LogoutActions.LoggedOut:
        this.message.next('You have successfully logged out!');
        break;
      default:
        throw new Error(`Invalid action '${action}'`);
    }
  }

  private logout(returnUrl: string) {
    const state: INavigationState = { returnUrl };
    const isAuthenticated = this.authorizeService
      .isAuthenticated()
      .pipe(
        filter((isAuthenticated) => {
          if (!isAuthenticated) {
            this.message.next('You have successfully logged out!');
          }
          return isAuthenticated;
        }),
        mergeMap(() => this.authorizeService.signOut(state)),
        take(1),
      )
      .subscribe((result) => {
        switch (result.status) {
          case AuthenticationResultStatus.Redirect:
            break;
          case AuthenticationResultStatus.Success:
            this.navigateToReturnUrl(returnUrl).pipe(take(1)).subscribe();
            break;
          case AuthenticationResultStatus.Fail:
            this.message.next(result.message);
            break;
          default:
            throw new Error('Invalid authentication result status.');
        }
      });
  }

  private processLogoutCallback() {
    this.authorizeService
      .completeSignOut(window.location.href)
      .pipe(take(1))
      .subscribe((result) => {
        switch (result.status) {
          case AuthenticationResultStatus.Redirect:
            // There should not be any redirects as the only time completeAuthentication finishes
            // is when we are doing a redirect sign in flow.
            throw new Error('Should not redirect.');
          case AuthenticationResultStatus.Success:
            this.navigateToReturnUrl(this.getReturnUrl(result.state));
            break;
          case AuthenticationResultStatus.Fail:
            this.message.next(result.message);
            break;
          default:
            throw new Error('Invalid authentication result status.');
        }
      });
  }

  private navigateToReturnUrl(returnUrl: string): Observable<boolean> {
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
    // If the url is coming from the query string, check that is either
    // a relative url or an absolute url
    if (fromQuery && !(fromQuery.startsWith(`${window.location.origin}/`) || /\/[^\/].*/.test(fromQuery))) {
      // This is an extra check to prevent open redirects.
      throw new Error('Invalid return url. The return url needs to have the same origin as the current page.');
    }
    return (state && state.returnUrl) || fromQuery || ApplicationPaths.LoggedOut;
  }
}

interface INavigationState {
  [ReturnUrlType]: string;
}
