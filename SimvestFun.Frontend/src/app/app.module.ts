import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { DecimalPipe } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RegisterComponent } from './components/register/register.component'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './routing.module';
import { LoginComponent } from './components/login/login.component';
import { AuthenticatedGuard } from './utils/authenticated.guard';
import { TokenInterceptor } from './utils/token.interceptor';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { ApplicationHeaderComponent } from './components/application-header/application-header.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSnackBarModule, MAT_SNACK_BAR_DEFAULT_OPTIONS } from '@angular/material/snack-bar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { BuyDialogComponent } from './components/buy-dialog/buy-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { AccountPageComponent } from './components/account-page/account-page.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { SellDialogComponent } from './components/sell-dialog/sell-dialog.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AllUsersComponent } from './components/users/users.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatExpansionModule } from '@angular/material/expansion';
import { LeaderboardComponent } from './components/leaderboard/leaderboard.component';
import { StocksComponent } from './components/stocks/stocks.component';
import { GravatarDirective } from './components/gravatar-directive';
import { UserActionHistoryComponent } from './components/user-action-history/user-action-history.component';
import { StockComponent } from './components/stock/stock.component';
import { NgChartsModule } from 'ng2-charts';
import { ChartComponent } from './components/chart/chart.component';
import { Title } from '@angular/platform-browser';
import { AboutPageComponent } from './components/about-page/about-page.component';
import { ApplicationFooterComponent } from './components/application-footer/application-footer.component';
import { ConfirmResetAccountDialogComponent } from './components/confirm-reset-account-dialog/confirm-reset-account-dialog.component';
import { RecentActionsComponent } from './components/recent-actions/recent-actions.component';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { MatMenuModule } from '@angular/material/menu';
import { ChangeAvatarDialogComponent } from './components/change-avatar-dialog/change-avatar-dialog.component';
import { UpdateUserDetailsDialogComponent } from './components/update-user-details-dialog/update-user-details-dialog.component';
import { FollowBoardComponent } from './components/followboard/followboard.component';
import { AdminPage } from './components/admin-page/admin-page.component';
import { LoadingSpinner } from './components/loading-spinner/loading-spinner.component';
import { SpinnerInterceptor } from './utils/loading-spinner.interceptor';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { ShareBlurbComponent } from './components/share-blurb/share-blurb.component';
import { UnsubscribePageComponent } from './components/unsubscribe-page/unsubscribe-page.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    ApplicationHeaderComponent,
    BuyDialogComponent,
    ConfirmDialogComponent,
    AccountPageComponent,
    StocksComponent,
    LeaderboardComponent,
    AllUsersComponent,
    SellDialogComponent,
    GravatarDirective,
    UserActionHistoryComponent,
    AboutPageComponent,
    ApplicationFooterComponent,
    StockComponent,
    ChartComponent,
    RecentActionsComponent,
    ConfirmResetAccountDialogComponent,
    ChangeAvatarDialogComponent,
    UpdateUserDetailsDialogComponent,
    AdminPage,
    FollowBoardComponent,
    LoadingSpinner,
    ShareBlurbComponent,
    UnsubscribePageComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    MatTableModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    AppRoutingModule,
    MatCardModule,
    MatDividerModule,
    MatIconModule,
    MatToolbarModule,
    MatSnackBarModule,
    MatProgressBarModule,
    MatDialogModule,
    MatSidenavModule,
    MatListModule,
    MatGridListModule,
    FlexLayoutModule,
    MatPaginatorModule,
    MatExpansionModule,
    NgChartsModule,
    SocialLoginModule,
    MatMenuModule,
    FormsModule,
    MatProgressSpinnerModule,
  ],
  providers: [
    DecimalPipe,
    AuthenticatedGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 2500 }
    },
    {
      provide: "SocialAuthServiceConfig",
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '255777409976-1rto2gjpdstcvledduduvpcssbjfrpom.apps.googleusercontent.com'
            )
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider(
              '706491120797797'
            )
          }
        ]
      } as SocialAuthServiceConfig
    },
    Title,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: SpinnerInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
