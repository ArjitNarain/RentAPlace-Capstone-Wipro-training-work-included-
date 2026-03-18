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

const routes: Routes = [

  { path: '', component: Home, pathMatch: 'full' },

  { path: 'login', component: Login },

  { path: 'register', component: Register },

  { path: 'properties', component: PropertyList },

  { path: 'property-details/:id', component: PropertyDetails },

  { path: 'add-property', component: AddProperty },

  { path: 'edit-property/:id', component: AddProperty },

  { path: 'reservations', component: Reservations },

  { path: 'messages', component: Messages }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
