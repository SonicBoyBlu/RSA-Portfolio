// Save current cart to localStorage
export const saveCart = async (cart: ICart) => {
  try {
    localStorage.setItem("cart-current", JSON.stringify(cart));
    return true;
  } catch {
    console.error("Error saving cart!");
    return false;
  }
};
