export default defineEventHandler(async (e) => {
  const req = await readBody(e);
  let _category: string = req.category || "";
  let _query: string = req.query || "";

  if (_category.toLowerCase() === "all") _category = "";

  const data: IProduct[] = await $fetch("https://fakestoreapi.com/products");
  let items: IProduct[] = data;

  // Filter to a specific category
  if (_category !== "") {
    items = data.filter((i) => i.category.toLowerCase() === _category);
  }

  // Filter by keyword in filtered category
  items = items.filter((i) =>
    i.title.toLowerCase().includes(_query.toLowerCase())
  );

  return items;
});
