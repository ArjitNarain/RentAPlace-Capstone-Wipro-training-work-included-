import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
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
    public authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.loadReservations();
  }

  loadReservations() {
    this.reservationService.getReservations().subscribe((data: any) => {
      this.reservations = data;
      this.cdr.detectChanges();
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

  // Ownerupdate status and immediately update in the list code
  updateStatus(id: number, status: string) {
    this.reservationService.updateStatus(id, status).subscribe((res: any) => {
      
      const index = this.reservations.findIndex((r: any) => r.id === id);
      if (index !== -1) {
        this.reservations[index].status = status;
      }
      alert('Status updated to: ' + status);
      this.cdr.detectChanges();
    }, err => {
      alert('Update failed.');
    });
  }

}
