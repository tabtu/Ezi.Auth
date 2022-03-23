package com.ezi.client.func;

public class Request {

    // private UDMMKV ud;
    private int validTokenLength = 25;
    private int timeout = 5;

    private const String USR = "usr";
    private const String PWD = "pwd";
    private const String HIDE = "hd";
    private const String SITE = "st";
    private const String _LOGIN = "https://api.yjezimoc.com/login";

    
    // Create json string from JsonObject
    public String CreateJsonParam(JsonObject param) {
        return "\"" + param.toString().replace('\"', '\'') + "\"";
    }

    // Request a token from server
    public String SyncToken(String appId, String secret, String siteId, String binding) {
        try {
            JsonObject json = new JsonObject();
            json.addProperty(USR, appId);
            json.addProperty(PWD, secret);
            json.addProperty(SITE, siteId);
            json.addProperty(HIDE, binding);
            String response = _SynHttpPost(_Login, CreateJsonParam(json));
            return response;
        } catch(Exception e) {
            return "";
        }
    }

    public String SyncAnonymousToken(String sites) {
        return SyncToken(Api.Anonymous, sites);
    }

    /**
     * Post Method Sync
     * @param urls
     * @param json
     * @return
     */
    public String _SynHttpPost(String urls, String json) {
        URL url = null;
        String result = null;
        HttpURLConnection connection = null;
        InputStreamReader in = null;
        try {
            url = new URL(urls);
            connection = (HttpURLConnection) url.openConnection();
            connection.setDoInput(true);
            connection.setDoOutput(true);
            connection.setRequestMethod("POST");
            connection.setRequestProperty("Content-Type", "application/json");
            connection.setRequestProperty("Charset", "utf-8");
            DataOutputStream dop = new DataOutputStream(connection.getOutputStream());
            dop.writeBytes(json);
            dop.flush();
            dop.close();
            in = new InputStreamReader(connection.getInputStream());
            BufferedReader bufferedReader = new BufferedReader(in);
            StringBuffer strBuffer = new StringBuffer();
            String line = null;
            while ((line = bufferedReader.readLine()) != null) {
                strBuffer.append(line);
            }
            result = strBuffer.toString();
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (connection != null) {
                connection.disconnect();
            }
            if (in != null) {
                try {
                    in.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
        return result;
    }

    /**
     * Post Method Async
     * @param urls
     * @param json
     */
    public void _AsyHttpPost(String urls, String json) {
        final String urlstr = urls;
        final String jsonstr = json;
        new Thread(new Runnable() {
            @Override
            public void run() {
                _SynHttpPost(urlstr, jsonstr);
            }
        }).start();
    }

    public String _SyncHttpJsonContent(String path){
        try {
            URL url = new URL(path);
            HttpURLConnection connection = (HttpURLConnection)url.openConnection();
            connection.setConnectTimeout(timeout*1000);
            connection.setRequestMethod("GET");
            connection.setDoInput(true);
            int code = connection.getResponseCode();
            if(code == 200) {
                return changeInputStream(connection.getInputStream());
            }
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }
}