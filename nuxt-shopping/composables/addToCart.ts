export const addToCart = async (item: IProduct) => {
  let _cart = await useMyCart();
  const x = _cart.items.findIndex((i) => i.id === item.id);
  if (x !== -1) {
    _cart.items[x] = item;
  } else {
    _cart.items.push(item);
  }
  await calculateCart(_cart);
  useState<ICart>("my-cart", () => _cart);

  return false;
};
