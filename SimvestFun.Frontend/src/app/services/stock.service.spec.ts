import { TestBed } from '@angular/core/testing';

import { StocksService } from './stock.service';

describe('StocksService', () => {
  let service: StocksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StocksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
