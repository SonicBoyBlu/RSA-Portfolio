export const useMyAccount = async () => {
  const user = await useCurrentUser();
  const addresses = await useMyAddresses();
  const orders = await useMyOrders();
  const paymentCards = await useMyPaymentCards();

  const me: IMyAccount = {
    user,
    addresses,
    orders,
    paymentCards,
  };
  me.user = user;

  const _state = useState<IMyAccount>("my-account");
  _state.value = me;
  return me;
};
