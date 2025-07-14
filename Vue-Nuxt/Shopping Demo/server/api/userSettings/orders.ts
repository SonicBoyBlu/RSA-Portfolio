export default defineEventHandler(async (e) => {
  const orders: ICart[] = [
    {
      orderId: 25000562,
      items: [
        {
          id: 1,
          title: "Fjallraven - Foldsack No. 1 Backpack, Fits 15 Laptops",
          price: 109.95,
          description:
            "Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday",
          category: "men's clothing",
          image: "https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg",
          rating: {
            rate: 3.9,
            count: 120,
          },
          qty: 1,
        },
        {
          id: 3,
          title: "Mens Cotton Jacket",
          price: 55.99,
          description:
            "great outerwear jackets for Spring/Autumn/Winter, suitable for many occasions, such as working, hiking, camping, mountain/rock climbing, cycling, traveling or other outdoors. Good gift choice for you or your family member. A warm hearted love to Father, husband or son in this thanksgiving or Christmas Day.",
          category: "men's clothing",
          image: "https://fakestoreapi.com/img/71li-ujtlUL._AC_UX679_.jpg",
          rating: {
            rate: 4.7,
            count: 500,
          },
          qty: 1,
        },
      ],
      taxRate: 0.0775,
      taxTotal: 21.381475,
      shipping: 9.99,
      subtotal: 275.89,
      total: 307.261475,
      paymentCard: {
        cardId: 2,
        cardType: "AMEX",
        label: "Black Card",
        nameOnCard: "Jane C Doe",
        accountNumber: "... 7963",
        accountDisplay: "... 7963",
        expMonth: 7,
        expYear: 29,
      },
      address: {
        addressId: 4,
        label: "Studio",
        recipient: "Starchild",
        street: "576 Park St",
        unitNum: "3D",
        city: "Funkytown",
        state: "CA",
        zip: "90000",
      },
    },
  ];
  return orders;
});
