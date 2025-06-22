// Used to update calculations on current cart
export const calculateCart = async (cart: ICart) => {
  const _cart = cart;
  //const _cart = await useMyCart();

  for (var i = 0; i < _cart.items.length; i++) {
    _cart.subtotal += _cart.items[i].price * _cart.items[i].qty;
  }
  _cart.taxTotal = _cart.subtotal * _cart.taxRate;
  _cart.shipping = _cart.subtotal > 0 ? 9.99 : 0;
  _cart.total = _cart.subtotal + _cart.shipping + _cart.taxTotal;
  saveCart(_cart);
  return _cart;
};
