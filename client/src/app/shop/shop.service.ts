import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/brands';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7016/api/'

  constructor(private http: HttpClient) { } 

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams()
      .set('itemsPerPage', '50')
      .set('pageNumber', '1');


    params = params.append('sort', shopParams.sort)
    
    if (shopParams.brandId !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }
  
    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.search){
      params = params.append('search', shopParams.search)
    }
    return this.http.get<IProduct[]>(this.baseUrl + 'products', { params });
  }   

  getProduct(id: number){
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id)
  }

  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands')
  }

  getTypes(){
    return this.http.get<IType[]>(this.baseUrl + 'products/types')
  }
}
