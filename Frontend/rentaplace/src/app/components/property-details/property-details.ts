import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../services/auth.service';
import { ReservationService } from '../../services/reservation.service';
import { MessageService } from '../../services/message.service';

@Component({
  selector: 'app-property-details',
  standalone: false,
  templateUrl: './property-details.html'
})
export class PropertyDetails implements OnInit {

  property: any = null;
  reviews: any[] = [];
  loaded = false;

  
  showReserveForm = false;
  checkIn = '';
  checkOut = '';

  // Review
  selectedRating = 0;
  comment = '';



  // Message
  messageContent = '';
  showMessageForm = false;





  constructor(
    private route: ActivatedRoute,
    public router: Router,
    private http: HttpClient,
    private reservationService: ReservationService,
    private messageService: MessageService,
    public authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.http.get('https://localhost:7287/api/Property/' + id).subscribe(
      (data: any) => {
        this.property = data;
        this.loaded = true;
        this.cdr.detectChanges();  // force Angular to update the view updated earlier it was not changing on its own refresh was needed

        // load reviews after property loads
        this.http.get('https://localhost:7287/api/Review/property/' + id).subscribe(
          (r: any) => {
            this.reviews = r;
            this.cdr.detectChanges();
          },
          () => { this.reviews = []; }
        );
      },
      () => { alert('Could not load property.'); }
    );
  }

  toggleReserveForm() {
    if (!this.authService.isLoggedIn()) {
      alert('Please login to make a reservation.');
      this.router.navigate(['/login']);
      return;
    }
    this.showReserveForm = !this.showReserveForm;
  }

  submitReservation() {
    if (!this.checkIn || !this.checkOut) {
      alert('Please select check-in and check-out dates.');
      return;
    }
    const reservation = {
      propertyId: this.property.id,
      checkIn: this.checkIn,
      checkOut: this.checkOut
    };
    this.reservationService.createReservation(reservation).subscribe(
      () => {
        alert('Reservation created successfully!');
        this.showReserveForm = false;
        this.checkIn = '';
        this.checkOut = '';
        this.router.navigate(['/reservations']);
      },
      () => { alert('Reservation failed. Please login first.'); }
    );
  }

  selectRating(star: number) {
    this.selectedRating = star;
  }

  submitReview() {
    if (this.selectedRating === 0) {
      alert('Please select a rating.');
      return;
    }
    const review = {
      propertyId: this.property.id,
      rating: this.selectedRating,
      comment: this.comment
    };
    this.http.post('https://localhost:7287/api/Review', review).subscribe(
      (res: any) => {
        alert('Review submitted!');
        this.reviews.push(res);
        this.comment = '';
        this.selectedRating = 0;
        this.cdr.detectChanges();
      },
      () => { alert('Failed. Please login first.'); }
    );
  }

  getAverage() {
    if (this.reviews.length === 0) return 'No ratings yet';
    const avg = this.reviews.reduce((sum: number, r: any) => sum + r.rating, 0) / this.reviews.length;
    return avg.toFixed(1) + ' / 5';
  }







  

  toggleMessageForm() {
    if (!this.authService.isLoggedIn()) {
      alert('Please login to send a message.');
      this.router.navigate(['/login']);
      return;
    }
    // Owner cannot message themselves
    if (this.authService.isOwner()) {
      alert('You are the owner. Use Messages tab to reply.');
      return;
    }
    this.showMessageForm = !this.showMessageForm;
  }

  sendMessage() {
    if (!this.messageContent.trim()) {
      alert('Please type a message.');
      return;
    }
    this.messageService.sendMessageToOwner(
      this.property.id,
      this.messageContent
    ).subscribe(
      () => {
        alert('Message sent to owner successfully!');
        this.messageContent = '';
        this.showMessageForm = false;
      },
      () => { alert('Failed to send message. Please login first.'); }
    );
  }







}
