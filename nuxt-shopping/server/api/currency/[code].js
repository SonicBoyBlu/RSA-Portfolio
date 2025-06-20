export default eventHandler(async (e) => {
    //const { code } = e.context.params;
    const code = "GBP";
    //const apiKeys = useRuntimeConfig().apiKeys;
    //const apiKey =  apiKeys.currencies;
    const apiKey = "cur_live_8jiwI7oH7wW74EVMR7fBXlbVDgWjhBJfI3K6En7A";
    const uri = "https://api.currencyapi.com/v3/latest?currencies=GBP&apikey=" + apiKey;
    const { data } = await $fetch(uri);

    return data;
})