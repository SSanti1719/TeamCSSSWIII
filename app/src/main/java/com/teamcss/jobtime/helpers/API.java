package com.teamcss.jobtime.helpers;

import android.content.Context;
import android.os.Build;
import android.os.StrictMode;
import android.util.Log;
import android.content.Context;
import android.widget.ArrayAdapter;

import com.teamcss.jobtime.model.Assign;
import com.teamcss.jobtime.model.Employee;
import com.teamcss.jobtime.model.Project;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;
import java.nio.charset.StandardCharsets;
import java.security.GeneralSecurityException;
import java.security.KeyStore;
import java.security.cert.Certificate;
import java.security.cert.CertificateFactory;
import java.security.cert.X509Certificate;

import javax.net.ssl.SSLContext;
import javax.net.ssl.SSLSocketFactory;
import javax.net.ssl.TrustManagerFactory;
import javax.security.cert.CertificateException;
import javax.net.ssl.HttpsURLConnection;

public class API {

    private final String URL = "http://10.0.2.2:5286/api/";
    private Context context;

    public API(Context context) {
        this.context = context;
    }

    public ArrayAdapter<Employee> getEmployees() throws IOException, GeneralSecurityException, CertificateException, JSONException {
        URL obj = new URL(URL + "Employee/GetEmployee");
        ArrayAdapter<Employee> employees = new ArrayAdapter<Employee>(this.context, android.R.layout.simple_list_item_1);

        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder()
                .permitAll().build();
        StrictMode.setThreadPolicy(policy);

        HttpURLConnection con = (HttpURLConnection) obj.openConnection();
        con.setRequestMethod("GET");
        con.connect();
        int responseCode = con.getResponseCode();
        System.out.println("GET Response Code :: " + responseCode);
        if (responseCode == HttpsURLConnection.HTTP_OK) { // success
            BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
            String inputLine;
            StringBuffer response = new StringBuffer();

            while ((inputLine = in.readLine()) != null) {
                JSONArray arrayResponse = new JSONArray(inputLine);
                for (int i = 0; i < arrayResponse.length(); i++) {
                    JSONObject jsonObject = arrayResponse.getJSONObject(i);
                    String document = jsonObject.getString("Nit");
                    String name = jsonObject.getString("Name");
                    String email = jsonObject.getString("Email");
                    employees.add(new Employee(document, name, email));
                }
            }
            in.close();
        } else {
            System.out.println("GET request did not work.");
        }

        return employees;
    }

    public ArrayAdapter<Project> getProjects() throws IOException, GeneralSecurityException, CertificateException, JSONException {
        URL obj = new URL(URL + "Project/GetProject");
        ArrayAdapter<Project> projects = new ArrayAdapter<Project>(this.context, android.R.layout.simple_list_item_1);

        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder()
                .permitAll().build();
        StrictMode.setThreadPolicy(policy);

        HttpURLConnection con = (HttpURLConnection) obj.openConnection();
        con.setRequestMethod("GET");
        con.connect();
        int responseCode = con.getResponseCode();
        System.out.println("GET Response Code :: " + responseCode);
        if (responseCode == HttpsURLConnection.HTTP_OK) { // success
            BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
            String inputLine;
            StringBuffer response = new StringBuffer();

            while ((inputLine = in.readLine()) != null) {
                JSONArray arrayResponse = new JSONArray(inputLine);
                for (int i = 0; i < arrayResponse.length(); i++) {
                    JSONObject jsonObject = arrayResponse.getJSONObject(i);
                    String id = jsonObject.getString("Id");
                    String nitClient = jsonObject.getString("NitClient");
                    String name = jsonObject.getString("Name");
                    String salesHours = jsonObject.getString("SalesHours");
                    projects.add(new Project(id, nitClient, name, Integer.parseInt(salesHours)));
                }
            }
            in.close();
        } else {
            System.out.println("GET request did not work.");
        }

        return projects;
    }

    public void saveAssign(Assign assign) throws IOException {
        URL obj = new URL(URL + "PostAssign");
        String urlParameters  = "NitProjectManager=a&NitEmployee=b&NitProject=c";
        byte[] postData = urlParameters.getBytes( StandardCharsets.UTF_8 );
        int postDataLength = postData.length;

        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder()
                .permitAll().build();
        StrictMode.setThreadPolicy(policy);

        HttpURLConnection con = (HttpURLConnection) obj.openConnection();
        con.setDoOutput( true );
        con.setInstanceFollowRedirects( false );
        con.setRequestMethod( "POST" );
        con.setRequestProperty( "Content-Type", "application/x-www-form-urlencoded");
        con.setRequestProperty( "charset", "utf-8");
        con.setRequestProperty( "Content-Length", Integer.toString( postDataLength ));
        con.setUseCaches( false );
        try( DataOutputStream wr = new DataOutputStream( con.getOutputStream())) {
            wr.write(postData);
        }
        con.connect();
        int responseCode = con.getResponseCode();
        if (responseCode == HttpsURLConnection.HTTP_OK) { // success

        } else {
            System.out.println("GET request did not work.");
        }
    }
}
