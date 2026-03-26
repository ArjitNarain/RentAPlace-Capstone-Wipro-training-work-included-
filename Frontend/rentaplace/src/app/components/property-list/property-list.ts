import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { PropertyService } from '../../services/property.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-property-list',
  standalone: false,
  templateUrl: './property-list.html'
})
export class PropertyList implements OnInit {

  properties: any[] = [];
  location: string = '';
  type: string = '';

  constructor(
    private propertyService: PropertyService,
    private router: Router,
    public authService: AuthService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties() {
    this.propertyService.getProperties().subscribe((data: any[]) => {
      this.properties = data;
      this.cdr.detectChanges();
    });
  }

  search() {
    this.propertyService.searchProperties(this.location, this.type)
      .subscribe((data: any) => {
        this.properties = data;
        this.cdr.detectChanges();
      });
  }

  viewDetails(id: number) {
    this.router.navigate(['/property-details', id]);
  }

  // Owner CRUD - Edit
  editProperty(id: number) {
    this.router.navigate(['/edit-property', id]);
  }

  // Owner CRUD - Delete
  deleteProperty(id: number) {
    if (confirm('Delete this property?')) {
      this.propertyService.deleteProperty(id).subscribe((res: any) => {
        alert('Property deleted');

        this.loadProperties();
        
      }, err => {
        alert('Delete failed');
      });
    }
  }

}
