import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountPageComponent } from './components/account-page/account-page.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { AllUsersComponent } from './components/users/users.component';
import { AboutPageComponent } from './components/about-page/about-page.component';
import { StockComponent } from './components/stock/stock.component';
import { RecentActionsComponent } from './components/recent-actions/recent-actions.component';
import { AdminPage } from './components/admin-page/admin-page.component';
import { UnsubscribePageComponent } from './components/unsubscribe-page/unsubscribe-page.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'register/:pass', component: RegisterComponent},
    { path: 'register', component: RegisterComponent },
    { path: 'login/forgot-password/:email', component: ForgotPasswordComponent},
    { path: 'login/forgot-password', component: ForgotPasswordComponent},
    { path: 'login/:pass', component: LoginComponent},
    { path: 'login', component: LoginComponent },
    { path: 'user/:userId', component: AccountPageComponent},
    { path: 'users', component: AllUsersComponent},
    { path: 'stock/:stockId', component: StockComponent},
    { path: 'recent-user-actions', component: RecentActionsComponent},
    { path: 'about', component: AboutPageComponent},
    { path: 'admin', component: AdminPage},
    { path: 'unsubscribe/:guid', component: UnsubscribePageComponent},
    { path: 'reset-password/:guid', component: ResetPasswordComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }