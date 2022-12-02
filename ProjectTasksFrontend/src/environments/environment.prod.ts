export const environment = {
    production: true,
    apiUrl: `https://projecttasksapilinuxappservice.azurewebsites.net/`,
    isReadonly: false,
    aadAuth: {
        scopes: ['api://431ea18b-eaeb-45c3-8351-5c770d777933/access_as_user'],
        redirectUri: "https://brave-ground-025ca3103.2.azurestaticapps.net/",
        clientId: "431ea18b-eaeb-45c3-8351-5c770d777933",
        loginAuthority: "https://login.microsoftonline.com/common",
    },
};
