export const useMyOrders = async () => {
  //let orders: ICart[] = [];
  const orders: ICart[] = await $fetch("/api/userSettings/orders");

  /*  if (orders.length === 0) orders = _orders;
  else orders.push(_orders);

  onMounted(async () => {
    try {
      console.log("Loding saved orders....");
      const _savedOrders:ICart[] = JSON.parse(localStorage.getItem("order-history"));
      orders.push(_savedOrders);
      console.log(_savedOrders.length + " saved orders loaded!");
    } catch {}
    try {
      console.log("Fetching orders from API...");
      const _orders = await $fetch("/api/userSettings/orders");
      orders.push(_orders);
      console.log(_orders.length + " fetch orders from API loaded!");
    } catch {}
  });
  */

  return orders;
};
