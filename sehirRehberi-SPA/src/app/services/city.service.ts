import { Photo } from './../models/photo';
import { City } from './../models/city';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  constructor(private httpClient:HttpClient) { }
  path="https://localhost:44334/api/";

  getCities():Observable<City[]>{
    return this.httpClient.get<City[]>(this.path+"Cities");
  }
  getCityById(cityId):Observable<City>{
    return this.httpClient.get<City>(this.path+"cities/details/?id="+cityId);
  }
  getPhotosByCity(cityId):Observable<Photo[]>{
    return this.httpClient.get<Photo[]>(this.path + "cities/photos/?cityId="+cityId);
  }
  addCity(city){
    this.httpClient.post(this.path+'cities/add',city).subscribe();
  }
}
