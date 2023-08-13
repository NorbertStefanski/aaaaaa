import {Cocktail} from "./cocktail.model";

describe('Cocktail', () => {
  let cocktail: Cocktail;

  beforeEach(() => {
    cocktail = new Cocktail('123', 'Mojito', 7.5, 'Rum');
  });

  it('should create an instance of Cocktail', () => {
    expect(cocktail).toBeTruthy();
  });

  it('should set the serialNumber property correctly', () => {
    expect(cocktail.serialNumber).toEqual('123');
  });

  it('should set the name property correctly', () => {
    expect(cocktail.name).toEqual('Mojito');
  });

  it('should set the purchasePrice property correctly', () => {
    expect(cocktail.purchasePrice).toEqual(7.5);
  });

  it('should set the category property correctly', () => {
    expect(cocktail.category).toEqual('Rum');
  });
});
