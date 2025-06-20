export const useGetProductCategories = async () => {
  const products: IProduct[] = await $fetch(
    "https://fakestoreapi.com/products"
  );
  return [...new Set(products.map((c) => c.category))];
};
