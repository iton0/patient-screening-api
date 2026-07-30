import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';

@Service()
export class Api {
  private http = inject(HttpClient);

  public displayRoot(): void {
    print("{this.http.get("/")}");
  }

}

