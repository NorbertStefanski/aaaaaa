import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./components/login/login.component";
import { MenuComponent } from './components/menu/menu.component';
import { PopupbarViewComponent } from './components/popupbar-view/popupbar-view.component';
import { PopupbarMenuComponent } from './components/popupbar-menu/popupbar-menu.component';
import { Error404Component } from './components/error404/error404.component';
import { CanActivateViaAuthGuardService } from './services/can-activate-via-auth-guard.service';
import { OrderPageComponent } from './components/order-page/order-page.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'menu',
    component: MenuComponent,
    canActivate: [CanActivateViaAuthGuardService]
  },
  {
    path: 'popup',
    component: PopupbarViewComponent,
    canActivate: [CanActivateViaAuthGuardService]
  },
  {
    path: 'popup-menu/:id',
    component: PopupbarMenuComponent,
    canActivate: [CanActivateViaAuthGuardService]
  },
  {
    path: 'order',
    component: OrderPageComponent,
    canActivate: [CanActivateViaAuthGuardService]
  },
  {
    path: '**',
    component: Error404Component,
    canActivate: [CanActivateViaAuthGuardService]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
