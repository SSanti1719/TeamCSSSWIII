package com.teamcss.jobtime.ui.gallery;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.SpinnerAdapter;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;

import com.teamcss.jobtime.databinding.FragmentGalleryBinding;
import com.teamcss.jobtime.model.API;
import com.teamcss.jobtime.model.Employee;
import com.teamcss.jobtime.model.Project;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;

public class GalleryFragment extends Fragment {

    private FragmentGalleryBinding binding;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        GalleryViewModel galleryViewModel =
                new ViewModelProvider(this).get(GalleryViewModel.class);

        binding = FragmentGalleryBinding.inflate(inflater, container, false);
        View root = binding.getRoot();

        binding.spEmpleados.setAdapter((SpinnerAdapter) getEmployeeList());
        binding.spProyectos.setAdapter((SpinnerAdapter) getProjectList());
        return root;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }

    public ArrayAdapter<Employee> getEmployeeList() {
        try {
            API api = new API(getContext());
            api.sendGET();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        ArrayAdapter<Employee> employees = new ArrayAdapter<Employee>(getContext(), android.R.layout.simple_list_item_1);
        employees.add(new Employee("2466567", "Stiven Velez", "stiven@gmail.com"));
        employees.add(new Employee("3575675", "Fabian Palacios", "stiven@gmail.com"));

        return employees;
    }



    public ArrayAdapter<Project> getProjectList() {
        ArrayAdapter<Project> projects = new ArrayAdapter<Project>(getContext(), android.R.layout.simple_list_item_1);
        projects.add(new Project("1", "35", "Proyecto 1", 12));
        projects.add(new Project("2", "38", "Proyecto 2", 12));

        return projects;
    }
}