export const useMyAddresses = async () => {
  let addresses: IAddress[] = await $fetch("/api/userSettings/addresses");
  if (addresses.length === 0) addresses.push(_defaultAddress);
  return addresses;
};

export const _defaultAddress: IAddress = {
  addressId: 0,
  label: "Unknown",
  recipient: "",
  street: "",
  unitNum: "",
  city: "",
  state: "",
  zip: "",
  isDefault: false,
};
