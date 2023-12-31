import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent {
  id: string = ""
  email: string = ""
  address: string = ""

  profileService = inject(ProfileService);
  toastrService = inject(ToastrService);
  authService = inject(AuthService);
  router = inject(Router);

  ngOnInit(): void {
    this.profileService.getProfile().subscribe((data : any) => {
      this.id = data.id;
    });
  }

  updateProfile() {
    this.profileService.updateProfile(this.id, { email: this.email, address: this.address }).subscribe({
    next: () => {
      this.toastrService.success("Success updating profile!", '', {
        progressBar: true,
        timeOut: 2000,
      });
      this.email = "";
      this.address = "";

      this.authService.logout();
      this.router.navigate(['/login']);
    },
    error: (response: any) => {
      console.log(response);
      this.toastrService.error(response?.error?.message ?? "Unexpected error", '', {
        progressBar: true,
        timeOut: 2000,
      });
    }
  });
  }
}
