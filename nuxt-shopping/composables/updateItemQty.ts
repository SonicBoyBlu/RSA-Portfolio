export const updateItemQty = async (item: IProduct) => {
  let _cart = await useMyCart();
  const x = _cart.items.findIndex((i) => i.id === item.id);
  if (x !== -1) {
    _cart.items[x] = item;
  }
  calculateCart();
};
