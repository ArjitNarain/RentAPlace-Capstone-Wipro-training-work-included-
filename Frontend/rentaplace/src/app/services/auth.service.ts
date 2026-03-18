import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = "https://localhost:7287/api";

  constructor(private http: HttpClient) { }

  login(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + '/Auth/login', data);
  }

  register(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + '/Auth/register', data);
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  saveUser(user: any) {
    if (!user) return;
    localStorage.setItem('userId', user.id?.toString() || user.Id?.toString() || '');
    localStorage.setItem('userRole', user.role || user.Role || '');
    localStorage.setItem('userName', user.name || user.Name || '');
    console.log('Saved user role:', user.role || user.Role);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  getUserRole() {
    return localStorage.getItem('userRole');
  }

  getUserName() {
    return localStorage.getItem('userName');
  }

  getUserId() {
    return localStorage.getItem('userId');
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  
  isOwner(): boolean {
    const role = localStorage.getItem('userRole');
    return role?.toLowerCase() === 'owner';
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('userRole');
    localStorage.removeItem('userName');
  }

}
