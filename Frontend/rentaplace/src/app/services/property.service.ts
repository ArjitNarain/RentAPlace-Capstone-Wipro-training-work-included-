import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Property {
  id?: number;
  title: string;
  description: string;
  location: string;
  price: number;
  propertyType: string;
  imagePath?: string;
  ownerId?: number;
}

@Injectable({
  providedIn: 'root'
})
export class PropertyService {

  baseUrl = 'https://localhost:7287/api';

  constructor(private http: HttpClient) {}

  getProperties(): Observable<any> {
    return this.http.get(this.baseUrl + '/Property');
  }

  getProperty(id: number): Observable<any> {
    return this.http.get(this.baseUrl + '/Property/' + id);
  }

  searchProperties(location: string, type: string): Observable<any> {
    return this.http.get(this.baseUrl + '/Property/search?location=' + location + '&type=' + type);
  }

  addProperty(data: any): Observable<any> {
    return this.http.post(this.baseUrl + '/Property', data);
  }

  updateProperty(id: number, data: any): Observable<any> {
    return this.http.put(this.baseUrl + '/Property/' + id, data);
  }

  deleteProperty(id: number): Observable<any> {
    return this.http.delete(this.baseUrl + '/Property/' + id, { responseType: 'text' });
  }

}
