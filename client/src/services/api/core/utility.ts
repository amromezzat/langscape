export const sleep = async (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay * 1000);
    });
}