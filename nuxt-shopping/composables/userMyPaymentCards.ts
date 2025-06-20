export const useMyPaymentCards = async () => {
  let cards: IPaymentCard[] = await $fetch("/api/userSettings/paymentCards");
  if (cards.length === 0) cards.push(_defaultPaymentCard);
  return cards;
};

export const _defaultPaymentCard: IPaymentCard = {
  cardId: 0,
  cardType: "Unknown",
  label: "Unknown",
  nameOnCard: "",
  billingZip: "",
  accountNumber: "",
  accountDisplay: "",
  expMonth: 1,
  expYear: 2000,
  cv2: 0,
};
