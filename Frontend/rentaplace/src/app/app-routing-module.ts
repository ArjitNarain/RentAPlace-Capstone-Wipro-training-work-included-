import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { PropertyList } from './components/property-list/property-list';
import { PropertyDetails } from './components/property-details/property-details';
import { AddProperty } from './components/add-property/add-property';
import { Reservations } from './components/reservations/reservations';
import { Messages } from './components/message/messages';
import { Home } from './home/home';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [

  { path: '', component: Home, pathMatch: 'full' },

  { path: 'login', component: Login },

  { path: 'register', component: Register },

  { path: 'properties', component: PropertyList },

  { path: 'property-details/:id', component: PropertyDetails },

  { path: 'add-property', component: AddProperty,   canActivate: [AuthGuard] },

  { path: 'edit-property/:id', component: AddProperty,   canActivate: [AuthGuard] },

  { path: 'reservations', component: Reservations,   canActivate: [AuthGuard] },

  { path: 'messages', component: Messages,   canActivate: [AuthGuard] }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
