import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/brands';
import { IType } from '../shared/models/productType';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7016/api/'

  constructor(private http: HttpClient) { } 

  getProducts(brandId?: number, typeId?: number) {
    let params = new HttpParams()
      .set('sort', 'nameAsc')
      .set('itemsPerPage', '50')
      .set('pageNumber', '1');
  
    if (brandId) {
      params = params.append('brandId', brandId.toString());
    }
  
    if (typeId) {
      params = params.append('typeId', typeId.toString());
    }
  
    return this.http.get<IProduct[]>(this.baseUrl + 'products', { params });
  }  

  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands')
  }

  getTypes(){
    return this.http.get<IType[]>(this.baseUrl + 'products/types')
  }
}
