import { RegisterComponent } from './register/register.component';
import { CityAddComponent } from './city/city-add/city-add.component';
import { Routes } from '@angular/router';
import { ValueComponent } from './value/value.component';
import { CityComponent } from './city/city.component';
import { CityDetailComponent } from './city/city-detail/city-detail.component';
export const  appRoutes :Routes = [
 {path:"city", component:CityComponent},
 {path:"value", component:ValueComponent},
 {path:"cityDetails/:cityId", component:CityDetailComponent},
 {path:"cityAdd",component:CityAddComponent},
 {path:"register", component:RegisterComponent},
 {path:"**", redirectTo:"city",pathMatch:"full"}
];

