import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  baseUrl = 'https://localhost:7287/api/Review';

  constructor(private http: HttpClient) {}

  getReviews(propertyId: number): Observable<any> {
    return this.http.get(this.baseUrl + '/property/' + propertyId);
  }

  addReview(review: any): Observable<any> {
    return this.http.post(this.baseUrl, review);
  }

}
