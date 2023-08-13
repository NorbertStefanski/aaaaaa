import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import {FormsModule} from "@angular/forms";
import { MenuComponent } from './components/menu/menu.component';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './components/navbar/navbar.component';
import { PopupbarViewComponent } from './components/popupbar-view/popupbar-view.component';
import { PopupbarMenuComponent } from './components/popupbar-menu/popupbar-menu.component';
import { Error404Component } from './components/error404/error404.component';
import { AuthService } from './services/auth.service';
import { CanActivateViaAuthGuardService } from './services/can-activate-via-auth-guard.service';
import { OrderPageComponent } from './components/order-page/order-page.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MenuComponent,
    NavbarComponent,
    PopupbarViewComponent,
    PopupbarMenuComponent,
    Error404Component,
    OrderPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [AuthService, CanActivateViaAuthGuardService],
  bootstrap: [AppComponent]
})
export class AppModule { }
