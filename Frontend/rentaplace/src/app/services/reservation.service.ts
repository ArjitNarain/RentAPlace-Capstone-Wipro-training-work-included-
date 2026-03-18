import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  baseUrl = 'https://localhost:7287/api/Reservation';

  constructor(private http: HttpClient) {}

  getReservations() {
    return this.http.get(this.baseUrl);
  }

  createReservation(reservation: any) {
    return this.http.post(this.baseUrl, reservation);
  }

  updateStatus(id: number, status: string) {
    return this.http.put(this.baseUrl + '/' + id + '/status', { status });
  }

}
