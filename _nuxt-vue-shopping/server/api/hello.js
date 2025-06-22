export default defineEventHandler(async (e) => {
    const data = await readBody(e);   
    let { name } = getQuery(e);
    if (!name) name = data.name
    if(!name) name = "World";

    return{
        message: `Hello ${name}!`// You are ${data.age} years old.`
    }
})