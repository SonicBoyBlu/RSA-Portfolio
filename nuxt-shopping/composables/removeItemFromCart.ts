export const removeItemFromCart = async (id: number) => {
  const _cart = await useMyCart();
  const x = _cart.items.findIndex((i) => i.id === id);

  if (x !== -1) {
    _cart.items.splice(x, 1);
    await calculateCart(_cart);
    useState<ICart>("my-cart", () => _cart);
  }
};
