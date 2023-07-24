import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { Inject, Injectable, InjectionToken, Optional } from '@angular/core';
import {
  Observable,
  mergeMap as _observableMergeMap,
  throwError as _observableThrow,
  catchError as _observableCatch,
  of as _observableOf,
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
  products?: ProductDto[] | undefined;
  createEnabled?: boolean;
}
export interface IProductDetailVm {
  productId?: number;
  productName?: string | undefined;
  unitPrice?: number | undefined;
  supplierId?: number | undefined;
  supplierCompanyName?: string | undefined;
  categoryId?: number | undefined;
  categoryName?: string | undefined;
  discontinued?: boolean;
  editEnabled?: boolean;
  deleteEnabled?: boolean;
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
export interface FileResponse {
  data: Blob;
  status: number;
  fileName?: string;
  headers?: { [name: string]: any };
}
export interface IProductService {
  getAll(): Observable<ProductsListVm>;
  get(id: number): Observable<ProductDetailVm>;
  create(command: CreateProductCommand): Observable<number>;
  update(command: UpdateProductCommand): Observable<void>;
  delete(id: number): Observable<void>;
  download(): Observable<FileResponse>;
}
export interface IProblemDetails {
  type?: string | undefined;
  title?: string | undefined;
  status?: number | undefined;
  detail?: string | undefined;
  instance?: string | undefined;
  extensions?: { [key: string]: any };
}
export class ProblemDetails implements IProblemDetails {
  type?: string | undefined;
  title?: string | undefined;
  status?: number | undefined;
  detail?: string | undefined;
  instance?: string | undefined;
  extensions?: { [key: string]: any };
  constructor(data?: IProblemDetails) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) (<any>this)[property] = (<any>data)[property];
      }
    }
  }
  init(_data?: any) {
    if (_data) {
      this.type = _data['type'];
      this.title = _data['title'];
      this.status = _data['status'];
      this.detail = _data['detail'];
      this.instance = _data['instance'];
      if (_data['extensions']) {
        this.extensions = {} as any;
        for (let key in _data['extensions']) {
          if (_data['extensions'].hasOwnProperty(key)) (<any>this.extensions)![key] = _data['extensions'][key];
        }
      }
    }
  }

  static fromJS(data: any): ProblemDetails {
    data = typeof data === 'object' ? data : {};
    let result = new ProblemDetails();
    result.init(data);
    return result;
  }
  toJSON(): any {
    let data = {};
    data['type'] = this.type;
    data['title'] = this.title;
    data['status'] = this.status;
    data['detail'] = this.detail;
    data['instance'] = this.instance;
    if (this.extensions) {
      data['extensions'] = {};
      for (let key in this.extensions) {
        if (this.extensions.hasOwnProperty(key)) (<any>data['extensions'])[key] = this.extensions[key];
      }
    }
    return data;
  }
}

export class ProductDto implements IProductDto {
  productId?: number;
  productName?: string | undefined;
  unitPrice?: number | undefined;
  supplierId?: number | undefined;
  supplierCompanyName?: string | undefined;
  categoryId?: number | undefined;
  categoryName?: string | undefined;
  discontinued?: boolean;

  constructor(data?: IProductDto) {
    if (data) {
      // for each property in the passed in IProductDto
      for (var property in data) {
        // check if it is not an inherited property
        if (data.hasOwnProperty(property)) {
          // copy the property value from the passed in IProductDto to the current object.
          (<any>this)[property] = (<any>data)[property];
        }
      }
    }
  }

  // Initialize the object from passed in Json object
  init(_data?: any) {
    if (_data) {
      this.productId = _data['productId'];
      this.productName = _data['productName'];
      this.unitPrice = _data['unitPrice'];
      this.supplierId = _data['supplierId'];
      this.supplierCompanyName = _data['supplierCompanyName'];
      this.categoryId = _data['categoryId'];
      this.categoryName = _data['categoryName'];
      this.discontinued = _data['discontinued'];
    }
  }

  /// Converts from JSON object to ProductDto
  static fromJS(data: any): ProductDto {
    data = typeof data === 'object' ? data : {};
    let result = new ProductDto();
    result.init(data);
    return result;
  }

  /// Converts from ProductDto to json object to ProductDto
  toJSON(): any {
    var data = {};
    data['productId'] = this.productId;
    data['productName'] = this.productName;
    data['unitPrice'] = this.unitPrice;
    data['supplierId'] = this.supplierId;
    data['supplierCompanyName'] = this.supplierCompanyName;
    data['categoryId'] = this.categoryId;
    data['categoryName'] = this.categoryName;
    data['discontinued'] = this.discontinued;
    return data;
  }
}

export class ProductsListVm implements IProductsListVm {
  products?: ProductDto[] | undefined;
  createEnabled?: boolean;

  constructor(data?: IProductsListVm) {
    if (data) {
      // for each property in the passed in IProductsListVm
      for (var property in data) {
        // check if it is not an inherited property
        if (data.hasOwnProperty(property)) {
          // copy the property value from the passed in IProductsListVm to the current object.
          (<any>this)[property] = (<any>data)[property];
        }
      }
    }
  }

  // Initialize the object from passed in Json object
  init(_data?: any) {
    if (_data) {
      if (Array.isArray(_data['products'])) {
        this.products = [] as any;
        for (let item of _data['products']) this.products!.push(ProductDto.fromJS(item));
      }
      this.createEnabled = _data['createEnabled'];
    }
  }

  /// Converts from JSON object to ProductListVm
  static fromJS(data: any): ProductsListVm {
    data = typeof data === 'object' ? data : {};
    let result = new ProductsListVm();
    result.init(data);
    return result;
  }

  /// Converts from ProductListVm to json object to ProductListVm
  toJSON(): any {
    var data = {};
    if (Array.isArray(this.products)) {
      data['products'] = [];
      for (let item of this.products) {
        data['products'].push(item.toJSON());
      }
    }
    data['createEnabled'] = this.createEnabled;
    return data;
  }
}

export class ProductDetailVm implements IProductDetailVm {
  productId?: number;
  productName?: string | undefined;
  unitPrice?: number | undefined;
  supplierId?: number | undefined;
  supplierCompanyName?: string | undefined;
  categoryId?: number | undefined;
  categoryName?: string | undefined;
  discontinued?: boolean;
  editEnabled?: boolean;
  deleteEnabled?: boolean;

  constructor(data?: IProductDetailVm) {
    if (data) {
      // for each property in the passed in IProductDetailVm
      for (var property in data) {
        // check if it is not an inherited property
        if (data.hasOwnProperty(property)) {
          // copy the property value from the passed in IProductsListVm to the current object.
          (<any>this)[property] = (<any>data)[property];
        }
      }
    }
  }

  // Initialize a ProductDetailVm from a Json object
  init(_data?: any) {
    if (_data) {
      this.productId = _data['productId'];
      this.productName = _data['productName'];
      this.unitPrice = _data['unitPrice'];
      this.supplierId = _data['supplierId'];
      this.supplierCompanyName = _data['supplierCompanyName'];
      this.categoryId = _data['categoryId'];
      this.categoryName = _data['categoryName'];
      this.discontinued = _data['discontinued'];
      this.editEnabled = _data['editEnabled'];
      this.deleteEnabled = _data['deleteEnabled'];
    }
  }

  // create a ProductDetailVm from the Json object
  static fromJS(data: any): ProductDetailVm {
    data = typeof data === 'object' ? data : {};
    let result = new ProductDetailVm();
    result.init(data);
    return result;
  }

  // create a Json object from a ProductDetailVM
  toJSON(): any {
    var data = {};
    data['productId'] = this.productId;
    data['productName'] = this.productName;
    data['unitPrice'] = this.unitPrice;
    data['supplierId'] = this.supplierId;
    data['supplierCompanyName'] = this.supplierCompanyName;
    data['categoryId'] = this.categoryId;
    data['categoryName'] = this.categoryName;
    data['discontinued'] = this.discontinued;
    data['editEnabled'] = this.editEnabled;
    data['deleteEnabled'] = this.deleteEnabled;
    return data;
  }
}

export class CreateProductCommand implements ICreateProductCommand {
  productName?: string | undefined;
  unitPrice?: number | undefined;
  supplierId?: number | undefined;
  categoryId?: number | undefined;
  discontinued?: boolean;

  constructor(data?: ICreateProductCommand) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      this.productName = _data['productName'];
      this.unitPrice = _data['unitPrice'];
      this.supplierId = _data['supplierId'];
      this.categoryId = _data['categoryId'];
      this.discontinued = _data['discontinued'];
    }
  }

  static fromJS(data: any): CreateProductCommand {
    data = typeof data === 'object' ? data : {};
    let result = new CreateProductCommand();
    result.init(data);
    return result;
  }

  toJSON(): any {
    let data = {};
    data['productName'] = this.productName;
    data['unitPrice'] = this.unitPrice;
    data['supplierId'] = this.supplierId;
    data['categoryId'] = this.categoryId;
    data['discontinued'] = this.discontinued;
    return data;
  }
}
export class UpdateProductCommand implements IUpdateProductCommand {
  productId?: number;
  productName?: string | undefined;
  unitPrice?: number | undefined;
  supplierId?: number | undefined;
  categoryId?: number | undefined;
  discontinued?: boolean;

  constructor(data?: IUpdateProductCommand) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property)) (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      this.productId = _data['productId'];
      this.productName = _data['productName'];
      this.unitPrice = _data['unitPrice'];
      this.supplierId = _data['supplierId'];
      this.categoryId = _data['categoryId'];
      this.discontinued = _data['discontinued'];
    }
  }

  static fromJS(data: any): UpdateProductCommand {
    data = typeof data === 'object' ? data : {};
    let result = new UpdateProductCommand();
    result.init(data);
    return result;
  }

  toJSON(): any {
    let data = {};
    data['productId'] = this.productId;
    data['productName'] = this.productName;
    data['unitPrice'] = this.unitPrice;
    data['supplierId'] = this.supplierId;
    data['categoryId'] = this.categoryId;
    data['discontinued'] = this.discontinued;
    return data;
  }
}

function blobToText(blob: any): Observable<string> {
  return new Observable<string>((observer: any) => {
    if (!blob) {
      observer.next('');
      observer.complete();
    } else {
      let reader = new FileReader();
      reader.onload = (event) => {
        observer.next((<any>event.target).result);
        observer.complete();
      };
      reader.readAsText(blob);
    }
  });
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any }, result?: any): Observable<any> {
  if (result !== null && result !== undefined) return _observableThrow(() => result);
  else return _observableThrow(() => new SwaggerException(message, status, response, headers, null));
}
export class SwaggerException extends Error {
  message: string;
  status: number;
  response: string;
  headers: { [key: string]: any };
  result: any;

  constructor(message: string, status: number, response: string, headers: { [key: string]: any }, result: any) {
    super();

    this.message = message;
    this.status = status;
    this.response = response;
    this.headers = headers;
    this.result = result;
  }

  protected isSwaggerException = true;

  static isSwaggerException(obj: any): obj is SwaggerException {
    return obj.isSwaggerException === true;
  }
}

@Injectable({
  providedIn: 'root',
})
export class ProductService implements IProductService {
  private http: HttpClient;
  private baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
    this.http = http;
    this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : '';
  }

  getAll(): Observable<ProductsListVm> {
    let url_ = this.baseUrl + '/api/Products/GetAll';
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        Accept: 'application/json',
      }),
    };

    return this.http
      .request('get', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processGetAll(response_);
        }),
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processGetAll(<any>response_);
            } catch (e) {
              return <Observable<ProductsListVm>>_observableThrow(() => e);
            }
          } else return <Observable<ProductsListVm>>_observableThrow(() => response_);
        }),
      );
  }
  protected processGetAll(response: HttpResponseBase): Observable<ProductsListVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body : (<any>response).error instanceof Blob ? (<any>response).error : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          let result200: any = null;
          let resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
          result200 = ProductsListVm.fromJS(resultData200);
          return _observableOf(result200);
        }),
      );
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          return throwException('An unexpected server error occurred.', status, _responseText, _headers);
        }),
      );
    }

    return _observableOf<ProductsListVm>(<any>null);
  }
  get(id: number): Observable<ProductDetailVm> {
    let url_ = this.baseUrl + '/api/Products/Get/{id}';
    if (id === undefined || id === null) throw new Error("The parameter 'id' must be defined.");
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        Accept: 'application/json',
      }),
    };

    return this.http
      .request('get', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processGet(response_);
        }),
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processGet(<any>response_);
            } catch (e) {
              return <Observable<ProductDetailVm>>(<any>_observableThrow(() => e));
            }
          } else return <Observable<ProductDetailVm>>(<any>_observableThrow(() => response_));
        }),
      );
  }

  protected processGet(response: HttpResponseBase): Observable<ProductDetailVm> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body : (<any>response).error instanceof Blob ? (<any>response).error : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          let result200: any = null;
          let resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
          result200 = ProductDetailVm.fromJS(resultData200);
          return _observableOf(result200);
        }),
      );
    } else if (status === 404) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          let result404: any = null;
          let resultData404 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
          result404 = ProblemDetails.fromJS(resultData404);
          return throwException('A server side error occurred.', status, _responseText, _headers, result404);
        }),
      );
    } else {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          let resultdefault: any = null;
          let resultDatadefault = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
          resultdefault = ProblemDetails.fromJS(resultDatadefault);
          return throwException('A server side error occurred.', status, _responseText, _headers, resultdefault);
        }),
      );
    }
  }

  create(command: CreateProductCommand): Observable<number> {
    let url_ = this.baseUrl + '/api/Products/Create';
    url_ = url_.replace(/[?&]$/, '');

    const content_ = JSON.stringify(command);

    let options_: any = {
      body: content_,
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Accept: 'application/json',
      }),
    };

    return this.http
      .request('post', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processCreate(response_);
        }),
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processCreate(<any>response_);
            } catch (e) {
              return <Observable<number>>(<any>_observableThrow(() => e));
            }
          } else return <Observable<number>>(<any>_observableThrow(() => response_));
        }),
      );
  }

  protected processCreate(response: HttpResponseBase): Observable<number> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body : (<any>response).error instanceof Blob ? (<any>response).error : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 201) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          let result201: any = null;
          let resultData201 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
          result201 = resultData201 !== undefined ? resultData201 : <any>null;
          return _observableOf(result201);
        }),
      );
    } else if (status === 400) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          let result400: any = null;
          let resultData400 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
          result400 = ProblemDetails.fromJS(resultData400);
          return throwException('A server side error occurred.', status, _responseText, _headers, result400);
        }),
      );
    } else {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          let resultdefault: any = null;
          let resultDatadefault = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
          resultdefault = ProblemDetails.fromJS(resultDatadefault);
          return throwException('A server side error occurred.', status, _responseText, _headers, resultdefault);
        }),
      );
    }
  }
  update(command: UpdateProductCommand): Observable<void> {
    let url_ = this.baseUrl + '/api/Products/Update';
    url_ = url_.replace(/[?&]$/, '');

    const content_ = JSON.stringify(command);

    let options_: any = {
      body: content_,
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };

    return this.http
      .request('put', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processUpdate(response_);
        }),
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processUpdate(<any>response_);
            } catch (e) {
              return <Observable<void>>(<any>_observableThrow(() => e));
            }
          } else return <Observable<void>>(<any>_observableThrow(() => response_));
        }),
      );
  }

  protected processUpdate(response: HttpResponseBase): Observable<void> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body : (<any>response).error instanceof Blob ? (<any>response).error : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          return _observableOf<void>(<any>null);
        }),
      );
    } else {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          let resultdefault: any = null;
          let resultDatadefault = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
          resultdefault = ProblemDetails.fromJS(resultDatadefault);
          return throwException('A server side error occurred.', status, _responseText, _headers, resultdefault);
        }),
      );
    }
  }

  delete(id: number): Observable<void> {
    let url_ = this.baseUrl + '/api/Products/Delete/{id}';
    if (id === undefined || id === null) throw new Error("The parameter 'id' must be defined.");
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({}),
    };

    return this.http
      .request('delete', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processDelete(response_);
        }),
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processDelete(<any>response_);
            } catch (e) {
              return <Observable<void>>(<any>_observableThrow(() => e));
            }
          } else return <Observable<void>>(<any>_observableThrow(() => response_));
        }),
      );
  }

  protected processDelete(response: HttpResponseBase): Observable<void> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body : (<any>response).error instanceof Blob ? (<any>response).error : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          return _observableOf<void>(<any>null);
        }),
      );
    } else {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          let resultdefault: any = null;
          let resultDatadefault = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
          resultdefault = ProblemDetails.fromJS(resultDatadefault);
          return throwException('A server side error occurred.', status, _responseText, _headers, resultdefault);
        }),
      );
    }
  }
  download(): Observable<FileResponse> {
    let url_ = this.baseUrl + '/api/Products/Download';
    url_ = url_.replace(/[?&]$/, '');

    let options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        Accept: 'application/octet-stream',
      }),
    };

    return this.http
      .request('get', url_, options_)
      .pipe(
        _observableMergeMap((response_: any) => {
          return this.processDownload(response_);
        }),
      )
      .pipe(
        _observableCatch((response_: any) => {
          if (response_ instanceof HttpResponseBase) {
            try {
              return this.processDownload(<any>response_);
            } catch (e) {
              return <Observable<FileResponse>>(<any>_observableThrow(() => e));
            }
          } else return <Observable<FileResponse>>(<any>_observableThrow(() => response_));
        }),
      );
  }

  protected processDownload(response: HttpResponseBase): Observable<FileResponse> {
    const status = response.status;
    const responseBlob =
      response instanceof HttpResponse ? response.body : (<any>response).error instanceof Blob ? (<any>response).error : undefined;

    let _headers: any = {};
    if (response.headers) {
      for (let key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }
    if (status === 200 || status === 206) {
      const contentDisposition = response.headers ? response.headers.get('content-disposition') : undefined;
      const fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
      const fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
      return _observableOf({ fileName: fileName, data: <any>responseBlob, status: status, headers: _headers });
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(
        _observableMergeMap((_responseText) => {
          return throwException('An unexpected server error occurred.', status, _responseText, _headers);
        }),
      );
    }
    return _observableOf<FileResponse>(<any>null);
  }
}
