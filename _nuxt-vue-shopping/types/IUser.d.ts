export {};
declare global {
  interface IUser {
    userId: number;
    username: string;
    firstName: string;
    lastName: string;
    phone: string;
    isAuth: boolean;
  }
}
