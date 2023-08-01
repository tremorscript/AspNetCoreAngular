import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { Inject, Injectable, InjectionToken, Optional } from '@angular/core';
import {
  Observable,
} from 'rxjs';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');
export interface IProductDto {
  productId?: number;
  productName?: string | undefined;
  unitPrice?: number | undefined;
  supplierId?: number | undefined;
  supplierCompanyName?: string | undefined;
  categoryId?: number | undefined;
  categoryName?: string | undefined;
  discontinued?: boolean;
}

export interface IProductsListVm {
  products?: IProductDto[] | undefined;
  createEnabled?: boolean;
}

export interface ICreateProductCommand {
  productName?: string | undefined;
  unitPrice?: number | undefined;
  supplierId?: number | undefined;
  categoryId?: number | undefined;
  discontinued?: boolean;
}

export interface IUpdateProductCommand {
  productId?: number;
  productName?: string | undefined;
  unitPrice?: number | undefined;
  supplierId?: number | undefined;
  categoryId?: number | undefined;
  discontinued?: boolean;
}

export interface IProductService {
  getAll(): Observable<IProductsListVm>;
  get(id: number): Observable<IProductDto>;
  create(command: ICreateProductCommand): Observable<IProductDto>;
  update(command: IUpdateProductCommand): Observable<IProductDto>;
  delete(id: number): Observable<void>;
}

@Injectable({
  providedIn: 'root',
})
export class ProductService implements IProductService {
  private http: HttpClient;
  private baseUrl: string;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this.http = http;
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : '';
  }

  getAll(): Observable<IProductsListVm> {
    let url_ = this.baseUrl + '/api/Products/GetAll';
    url_ = url_.replace(/[?&]$/, '');

    return this.http.get<IProductsListVm>(url_);
  }

  get(id: number): Observable<IProductDto> {
    let url_ = this.baseUrl + '/api/Products/Get/{id}';
    if (id === undefined || id === null) throw new Error("The parameter 'id' must be defined.");
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');

    return this.http.get<IProductDto>(url_);
  }

  create(command: ICreateProductCommand): Observable<IProductDto> {
    let url_ = this.baseUrl + '/api/Products/Create';
    url_ = url_.replace(/[?&]$/, '');

    let option = {
        headers: new HttpHeaders({
          Accept: 'appplication/json'
        })
      }

    return this.http
      .post<IProductDto>(url_, command, option );
  }

  update(command: IUpdateProductCommand): Observable<IProductDto> {
    let url_ = this.baseUrl + '/api/Products/Update';
    url_ = url_.replace(/[?&]$/, '');

    let option = {
        headers: new HttpHeaders({
          Accept: 'appplication/json'
        })
      }

    return this.http
      .put<IProductDto>(url_, command, option);
  }

  delete(id: number): Observable<void> {
    let url_ = this.baseUrl + '/api/Products/Delete/{id}';
    if (id === undefined || id === null) throw new Error("The parameter 'id' must be defined.");
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');
    return this.http.delete<void>(url_);
  }
}
