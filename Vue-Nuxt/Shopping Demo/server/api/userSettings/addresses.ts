export default defineEventHandler(async (e) => {
  const addresses: IAddress[] = [
    {
      addressId: 1,
      label: "Home",
      recipient: "Jon Q Doe",
      street: "1234 Street Pl",
      //unitNum: "",
      city: "Townerton",
      state: "CA",
      zip: "90000",
      isDefault: true,
    },
    {
      addressId: 4,
      label: "Studio",
      recipient: "Starchild",
      street: "576 Park St",
      unitNum: "3D",
      city: "Funkytown",
      state: "CA",
      zip: "90000",
      //isDefault: true,
    },
    {
      addressId: 7,
      label: "Mom's",
      recipient: "C/O Momma Bear",
      street: "99634 Homestead Rd",
      //unitNum: "",
      city: "Lovesville",
      state: "AL",
      zip: "33000",
    },
  ];
  return addresses;
});
