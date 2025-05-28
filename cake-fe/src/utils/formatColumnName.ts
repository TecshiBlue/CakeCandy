export const formatColumnName = (columnName: string) => {
    const sanitizedValue = columnName
    .replace(/([A-Z])/g, " $1")  
    .trim()
    .split(" "); 

    const [first, ...rest] = sanitizedValue.map((value)=> value.toLowerCase());

    const restJoined = rest.join(" ");

    if (first === "id") return `Código de ${restJoined}`;

    const capitalizedFirst = first.charAt(0).toUpperCase() + first.slice(1);
    return restJoined
    ? `${capitalizedFirst} de ${restJoined}`
    : capitalizedFirst;
}