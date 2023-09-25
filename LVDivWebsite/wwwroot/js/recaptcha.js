async function getResponse()
{
    const response = await grecaptcha.execute('6Lc79u0nAAAAAOfSpXGcZhv-LnHO97WBBei-nHKI', { action: 'submit' }).then(function (token)
    {
        // Add your logic to submit to your backend server here.
        console.log("Token: ", token);
        return token;
    });

    return response;
};