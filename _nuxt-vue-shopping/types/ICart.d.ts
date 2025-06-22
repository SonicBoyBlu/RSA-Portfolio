export {};
declare global {
  interface ICart {
    orderId: number;
    address?: IAddress;
    paymentCard?: IPaymentCard;
    items: IProduct[];
    taxRate: number;
    taxTotal: number;
    shipping: number;
    status?: string;
    subtotal: number;
    total: number;
  }

  /*
  class Cart implements ICart {
    orderId: number;
    address?: IAddress;
    paymentCard?: IPaymentCard;
    items: IProduct[];
    taxRate: number;
    taxTotal: number;
    shipping: number;
    subtotal: number;
    total: number;

    /*
    get subtotal(): number {
      return 27.99;
    }
    get total(): number {
      return 29.99;
    }
      * /
  }
*/
}
