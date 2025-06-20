export {};
declare global {
  interface IProduct {
    id: number;
    title: string;
    price: number;
    description: string;
    category: string;
    image: string;
    qty: number;
    rating: {
      count: number;
      rate: number;
    };
  }
}
