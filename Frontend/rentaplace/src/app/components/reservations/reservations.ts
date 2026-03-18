import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../../services/reservation.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-reservations',
  standalone: false,
  templateUrl: './reservations.html'
})
export class Reservations implements OnInit {

  reservations: any[] = [];

  reservationData = {
    propertyId: '',
    checkIn: '',
    checkOut: ''
  };

  constructor(
    private reservationService: ReservationService,
    public authService: AuthService
  ) {}

  ngOnInit() {
    this.loadReservations();
  }

  loadReservations() {
    this.reservationService.getReservations().subscribe((data: any) => {
      this.reservations = data;
    });
  }

  createReservation() {
    this.reservationService.createReservation(this.reservationData).subscribe((res: any) => {
      alert('Reservation Created! Status: ' + res.status);
      this.loadReservations();
    }, err => {
      alert('Reservation Failed. Please login first.');
    });
  }

  // Owner: update status and immediately update in the list
  updateStatus(id: number, status: string) {
    this.reservationService.updateStatus(id, status).subscribe((res: any) => {
      // Find and update the reservation in the list immediately
      const index = this.reservations.findIndex((r: any) => r.id === id);
      if (index !== -1) {
        this.reservations[index].status = status;
      }
      alert('Status updated to: ' + status);
    }, err => {
      alert('Update failed.');
    });
  }

}
