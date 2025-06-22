export default defineEventHandler(async (e) => {
  const cards: IPaymentCard[] = [
    {
      cardId: 1,
      cardType: "Visa",
      label: "Pink Visa",
      nameOnCard: "Jon Q Doe",
      accountNumber: "... 9863",
      accountDisplay: "... 9863",
      expMonth: 12,
      expYear: 27,
      isDefault: true,
    },
    {
      cardId: 2,
      cardType: "AMEX",
      label: "Black Card",
      nameOnCard: "Jane C Doe",
      accountNumber: "... 7963",
      accountDisplay: "... 7963",
      expMonth: 7,
      expYear: 29,
    },
  ];

  return cards;
});
