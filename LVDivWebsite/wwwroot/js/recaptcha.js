grecaptcha.ready(
async function getResponse()
{
    const response = await grecaptcha.execute('6Lc79u0nAAAAAOfSpXGcZhv-LnHO97WBBei-nHKI', { action: 'submit' }).then(function (token)
    {
        console.log("Token: ", token);
        return token;
    });

    return response;
});