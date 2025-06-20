export {};
declare global {
  interface IMyAccount {
    user: IUser;
    addresses: IAddress[];
    orders: ICart[];
    paymentCards: PaymentCard[];
  }
}
