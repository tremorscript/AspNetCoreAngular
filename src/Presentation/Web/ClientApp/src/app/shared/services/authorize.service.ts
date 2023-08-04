import { Injectable } from '@angular/core';
import { User, UserManager, WebStorageStateStore } from 'oidc-client-ts';
import { BehaviorSubject, concat, defer, from, Observable, of, throwError, onErrorResumeNext } from 'rxjs';
import { catchError, filter, map, mergeMap, take, tap } from 'rxjs/operators';
import { ApplicationPaths, ApplicationName } from '../constants';

export type IAuthenticationResult = SuccessAuthenticationResult | FailureAuthenticationResult | RedirectAuthenticationResult;

export interface SuccessAuthenticationResult {
  status: AuthenticationResultStatus.Success;
  state: any;
}

export interface FailureAuthenticationResult {
  status: AuthenticationResultStatus.Fail;
  message: string;
}

export interface RedirectAuthenticationResult {
  status: AuthenticationResultStatus.Redirect;
}

export enum AuthenticationResultStatus {
  Success,
  Redirect,
  Fail,
}

export interface IUser extends User {
  name: string;
  role: string[];
}

@Injectable({
  providedIn: 'root',
})
export class AuthorizeService {
  // By default pop ups are disabled because they don't work properly on Edge.
  // If you want to enable pop up authentication simply set this flag to false.
  private _user: IUser;
  private popUpDisabled = true;
  private userManager: UserManager;
  private userSubject: BehaviorSubject<IUser | null> = new BehaviorSubject(null);

  get isLoggedIn(): boolean {
    return !!this.user;
  }

  get user(): IUser {
    return this._user;
  }

  public hasRole(role: string): boolean {
    return this._user && this._user.role && this._user.role.indexOf(role) > -1;
  }

  public isAuthenticated(): Observable<boolean> {
    return this.getUser().pipe(map((u) => !!u));
  }

  public getUser(): Observable<IUser | null> {
    return concat(
      this.userSubject.pipe(
        take(1),
        filter((u) => !!u),
      ),
      this.getUserFromStorage().pipe(
        filter((u) => !!u),
        tap((u) => {
          this._user = u;
          this.userSubject.next(u);
        }),
      ),
      this.userSubject.asObservable(),
    );
  }

  public getAccessToken(): Observable<string> {
    return from(this.ensureUserManagerInitialized()).pipe(
      mergeMap(() => from(this.userManager.getUser())),
      map((user) => user && user.access_token),
    );
  }

  // We try to authenticate the user in three different ways:
  // 1) We try to see if we can authenticate the user silently. This happens
  //    when the user is already logged in on the IdP and is done using a hidden iframe
  //    on the client.
  // 3) If the above method above fails, we redirect the browser to the IdP to perform a traditional
  //    redirect flow.
  public signIn(state: any): Observable<IAuthenticationResult> {
    let ensureUserManagerInitialized$ = defer(() => this.ensureUserManagerInitialized());
    return onErrorResumeNext(
      ensureUserManagerInitialized$.pipe(
        mergeMap(() => this.userManager.signinSilent(this.createArguments())),
        tap((user) => this.userSubject.next(user.profile as any)),
        map((user) => this.success(state)),
      ),
      ensureUserManagerInitialized$.pipe(
        mergeMap(() => this.userManager.signinRedirect(this.createArguments(state))),
        map(() => this.redirect()),
      ),
    );
  }

  public completeSignIn(url: string): Observable<IAuthenticationResult> {
    var ensureUserManagerInitialized$ = defer(() => from(this.ensureUserManagerInitialized()));
    return ensureUserManagerInitialized$.pipe(
      mergeMap(() => this.userManager.signinCallback(url)),
      tap((user) => this.userSubject.next(user && (user.profile as any))),
      map((user) => this.success(user && user.state)),
      catchError((err) => of(this.error('TEst'))),
    );
  }

  public signOut(state: any): Observable<IAuthenticationResult> {
    let ensureUserManagerInitialized$ = defer(() => from(this.ensureUserManagerInitialized()));
    return ensureUserManagerInitialized$.pipe(
      tap(() => this.userManager.signoutRedirect(this.createArguments())),
      map(() => this.redirect()),
      catchError((err) => of(this.error(err))),
    );
  }

  public completeSignOut(url: string): Observable<IAuthenticationResult> {
    let ensureUserManagerInitialized$ = defer(() => this.ensureUserManagerInitialized());
    try {
      return ensureUserManagerInitialized$.pipe(
        mergeMap(() => this.userManager.signoutCallback(url)),
        tap(() => this.userSubject.next(null)),
        map(() => this.success({})),
        catchError((err) => {
          return of(this.error(err));
        }),
      );
    } catch (error) {
      return throwError(() => this.error(error));
    }
  }

  private createArguments(state?: any): any {
    return { useReplaceToNavigate: true, data: state };
  }

  private error(message: string): IAuthenticationResult {
    return { status: AuthenticationResultStatus.Fail, message };
  }

  private success(state: any): IAuthenticationResult {
    return { status: AuthenticationResultStatus.Success, state };
  }

  private redirect(): IAuthenticationResult {
    return { status: AuthenticationResultStatus.Redirect };
  }

  private async ensureUserManagerInitialized(): Promise<void> {
    if (this.userManager !== undefined) {
      return;
    }
    const response = await fetch(ApplicationPaths.ApiAuthorizationClientConfigurationUrl);
    if (!response.ok) {
      throw new Error(`Could not load settings for '${ApplicationName}'`);
    }

    const settings: any = await response.json();
    settings.automaticSilentRenew = true;
    settings.includeIdTokenInSilentRenew = true;
    this.userManager = new UserManager(settings);

    this.userManager.events.addUserSignedOut(async () => {
      await this.userManager.removeUser();
      this.userSubject.next(null);
    });
  }

  private getUserFromStorage(): Observable<IUser> {
    return from(this.ensureUserManagerInitialized()).pipe(
      mergeMap(() => this.userManager.getUser()),
      map((u) => u && (u.profile as any)),
    );
  }
}
