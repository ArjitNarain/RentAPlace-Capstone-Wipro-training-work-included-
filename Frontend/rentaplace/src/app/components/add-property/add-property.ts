import { Component, OnInit } from '@angular/core';
import { PropertyService } from '../../services/property.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-property',
  standalone: false,
  templateUrl: './add-property.html'
})
export class AddProperty implements OnInit {

  editId: number | null = null;

  property: any = {
    title: '',
    description: '',
    location: '',
    price: 0,
    propertyType: '',
    imagePath: ''
  };

  uploadMessage = '';

  constructor(
    private propertyService: PropertyService,
    public router: Router,
    private route: ActivatedRoute,
    private http: HttpClient
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.editId = Number(id);
      this.propertyService.getProperty(this.editId).subscribe((data: any) => {
        this.property = data;
      });
    }
  }

  // Upload image to backend Uploads folder
  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (!file) return;

    const formData = new FormData();
    formData.append('file', file);

    this.uploadMessage = 'Uploading...';

    this.http.post<any>('https://localhost:7287/api/Upload', formData).subscribe(
      (res: any) => {
        // Save the full URL returned by backend
        this.property.imagePath = res.url;
        this.uploadMessage = 'Image uploaded: ' + file.name;
      },
      err => {
        this.uploadMessage = 'Upload failed. Make sure you are logged in.';
      }
    );
  }

  saveProperty() {
    if (!this.property.title || !this.property.location || !this.property.price) {
      alert('Please fill Title, Location and Price.');
      return;
    }

    if (this.editId) {
      this.propertyService.updateProperty(this.editId, this.property).subscribe((res: any) => {
        alert('Property updated successfully!');
        this.router.navigate(['/properties']);
      }, err => {
        alert('Update failed.');
      });
    } else {
      this.propertyService.addProperty(this.property).subscribe((res: any) => {
        alert('Property added successfully!');
        this.router.navigate(['/properties']);
      }, err => {
        alert('Error adding property.');
      });
    }
  }

}
