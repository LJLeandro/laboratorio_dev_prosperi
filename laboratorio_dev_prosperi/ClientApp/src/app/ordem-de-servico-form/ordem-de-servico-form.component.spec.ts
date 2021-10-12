import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrdemDeServicoFormComponent } from './ordem-de-servico-form.component';

describe('OrdemDeServicoFormComponent', () => {
  let component: OrdemDeServicoFormComponent;
  let fixture: ComponentFixture<OrdemDeServicoFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrdemDeServicoFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdemDeServicoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
