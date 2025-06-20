export {};
declare global {
  interface IPaymentCard {
    cardId: number;
    cardType: string;
    label: string;
    nameOnCard?: string;
    billingZip?: string;
    accountNumber?: string;
    accountDisplay: string;
    expMonth: number;
    expYear: number;
    cv2?: number;
    isDefault?: boolean;
  }
}
