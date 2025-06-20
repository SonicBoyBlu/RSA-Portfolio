export const useMyCart = async () => {
  let _cart: ICart = {
    orderId: 0,
    items: [],
    taxRate: 0.0775,
    taxTotal: 0,
    shipping: 0,
    subtotal: 0,
    total: 0,
  };
  try {
    const savedCart = localStorage.getItem("cart-current");
    if (savedCart) {
      //debugger;
      _cart = JSON.parse(savedCart);
    }
  } catch {
    console.error("Error occurred while loading saved cart!");
  }
  const _state = useState<ICart>("my-cart", () => _cart);

  //return useState<ICart>("my-cart", () => _cart);
  //return _cart;
  return _state.value;
};
