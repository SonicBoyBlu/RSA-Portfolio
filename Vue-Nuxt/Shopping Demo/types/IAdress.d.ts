export {};
declare global {
  interface IAddress {
    addressId: number;
    label: string;
    recipient: string;
    street: string;
    unitNum?: string;
    city: string;
    state: string;
    zip: string;
    isDefault?: boolean;
  }
}
